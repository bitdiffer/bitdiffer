using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.IO;

namespace BitDiffer.Common.Utility
{
    // Based from MSDN blog post http://blogs.msdn.com/haibo_luo/archive/2005/10/04/476242.aspx
    internal class MethodStreamer
    {
        private byte[] _bytes;
        private int _position;
        private MethodBase _method;
        private BinaryWriter _writer;

        static OpCode[] s_OneByteOpCodes = new OpCode[0x100];
        static OpCode[] s_TwoByteOpCodes = new OpCode[0x100];

        Byte ReadByte() { return (Byte)_bytes[_position++]; }
        SByte ReadSByte() { return (SByte)ReadByte(); }

        UInt16 ReadUInt16() { _position += 2; return BitConverter.ToUInt16(_bytes, _position - 2); }
        UInt32 ReadUInt32() { _position += 4; return BitConverter.ToUInt32(_bytes, _position - 4); }
        UInt64 ReadUInt64() { _position += 8; return BitConverter.ToUInt64(_bytes, _position - 8); }

        Int32 ReadInt32() { _position += 4; return BitConverter.ToInt32(_bytes, _position - 4); }
        Int64 ReadInt64() { _position += 8; return BitConverter.ToInt64(_bytes, _position - 8); }

        Single ReadSingle() { _position += 4; return BitConverter.ToSingle(_bytes, _position - 4); }
        Double ReadDouble() { _position += 8; return BitConverter.ToDouble(_bytes, _position - 8); }

        static MethodStreamer()
        {
            foreach (FieldInfo fi in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                OpCode opCode = (OpCode)fi.GetValue(null);
                UInt16 value = (UInt16)opCode.Value;
                if (value < 0x100)
                    s_OneByteOpCodes[value] = opCode;
                else if ((value & 0xff00) == 0xfe00)
                    s_TwoByteOpCodes[value & 0xff] = opCode;
            }
        }

        internal MethodStreamer(MethodBase method)
        {
            _method = method;
        }

        internal void WriteImplementationToStream(Stream stream)
        {
            _position = 0;
            _writer = new BinaryWriter(stream, Encoding.Unicode);
            _bytes = _method.GetMethodBody().GetILAsByteArray();

            WriteImplementationToStream();
        }

        private void WriteImplementationToStream()
        {
            OpCode opcode = OpCodes.Nop;

            while (_position < _bytes.Length)
            {
                byte op = ReadByte();
                _writer.Write(op);

                if (op != 0xFE)
                {
                    opcode = s_OneByteOpCodes[op];
                }
                else
                {
                    op = ReadByte();
                    _writer.Write(op);
                    opcode = s_TwoByteOpCodes[op];
                }

                switch (opcode.OperandType)
                {
                    case OperandType.InlineBrTarget:
                        _writer.Write(ReadInt32());
                        break;
                    case OperandType.InlineField:
                        _writer.Write(GetNameForField(ReadInt32()));
                        break;
                    case OperandType.InlineMethod:
                        _writer.Write(GetNameForMethod(ReadInt32()));
                        break;
                    case OperandType.InlineSig:
                        _writer.Write(_method.Module.ResolveSignature(ReadInt32()));
                        break;
                    case OperandType.InlineTok:
                        _writer.Write(GetNameForTok(ReadInt32()));
                        break;
                    case OperandType.InlineType:
                        _writer.Write(GetNameForType(ReadInt32()));
                        break;
                    case OperandType.InlineI:
                        _writer.Write(ReadInt32());
                        break;
                    case OperandType.InlineI8:
                        _writer.Write(ReadInt64());
                        break;
                    case OperandType.InlineNone:
                        break;
                    case OperandType.InlineR:
                        _writer.Write(ReadDouble());
                        break;
                    case OperandType.InlineString:
						int value = ReadInt32();
                        _writer.Write(_method.Module.ResolveString(value));
                        break;
                    case OperandType.InlineSwitch:
                        int count = ReadInt32();
                        _writer.Write(count);
                        for (int i = 0; i < count; i++)
                        {
                            _writer.Write(ReadInt32());
                        }
                        break;
                    case OperandType.InlineVar:
                        _writer.Write(ReadUInt16());
                        break;
                    case OperandType.ShortInlineBrTarget:
                        _writer.Write(ReadSByte());
                        break;
                    case OperandType.ShortInlineI:
                        _writer.Write(ReadSByte());
                        break;
                    case OperandType.ShortInlineR:
                        _writer.Write(ReadSingle());
                        break;
                    case OperandType.ShortInlineVar:
                        _writer.Write(ReadByte());
                        break;
                    default:
						throw new InvalidProgramException("Unknown IL instruction");
                }
            }
        }

        private string GetNameForField(int token)
        {
            if (_method is ConstructorInfo)
            {
                return GetNameForField(_method.Module.ResolveField(token, _method.DeclaringType.GetGenericArguments(), null));
            }
            else
            {
                return GetNameForField(_method.Module.ResolveField(token, _method.DeclaringType.GetGenericArguments(), _method.GetGenericArguments()));
            }
        }

        private string GetNameForMethod(int token)
        {
            try
            {
                if (_method is ConstructorInfo)
                {
                    return GetNameForMethod(_method.Module.ResolveMethod(token, _method.DeclaringType.GetGenericArguments(), null));
                }
                else
                {
                    return GetNameForMethod(_method.Module.ResolveMethod(token, _method.DeclaringType.GetGenericArguments(), _method.GetGenericArguments()));
                }
            }
            catch
            {
                if (_method is ConstructorInfo)
                {
                    return GetNameForMember(_method.Module.ResolveMember(token, _method.DeclaringType.GetGenericArguments(), null));
                }
                else
                {
                    return GetNameForMember(_method.Module.ResolveMember(token, _method.DeclaringType.GetGenericArguments(), _method.GetGenericArguments()));
                }
            }
        }

		private string GetNameForTok(int token)
		{
			try
			{
				return GetNameForType(token);
			}
			catch
			{
			}

			try
			{
				return GetNameForMethod(token);
			}
			catch
			{
			}

			try
			{
				return GetNameForField(token);
			}
			catch
			{
			}

			throw new ArgumentException("Unable to parse inline tok " + token.ToString());
		}

        private string GetNameForType(int token)
        {
            if (_method is MethodInfo)
            {
                return GetNameForType(_method.Module.ResolveType(token, _method.DeclaringType.GetGenericArguments(), _method.GetGenericArguments()));
            }
            else if (_method is ConstructorInfo)
            {
                return GetNameForType(_method.Module.ResolveType(token, _method.DeclaringType.GetGenericArguments(), null));
            }
            else
            {
                return GetNameForType(_method.Module.ResolveType(token));
            }
        }

        private string GetNameForField(FieldInfo fi)
        {
            return (fi == null) ? "" : fi.Name;
        }

        private string GetNameForMethod(MethodBase mb)
        {
            return (mb == null) ? "" : mb.Name;
        }

        private string GetNameForMember(MemberInfo mi)
        {
            return (mi == null) ? "" : mi.Name;
        }

        private string GetNameForType(Type type)
        {
			// Dont use FullName... for generics it includes a version number in referenced types, and if that referenced version changes
			// the output changes when it should not.

			if ((type.IsGenericType) && (type.DeclaringType != null))
			{
				return type.Name + ":" + type.DeclaringType.Name;
			}
			else
			{
				return type.Name;
			}
        }
    }
}
