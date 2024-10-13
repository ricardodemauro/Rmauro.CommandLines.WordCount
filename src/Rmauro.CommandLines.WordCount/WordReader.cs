using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Rmauro.CommandLines.WordCount;

public class WordReader(Stream stream, Encoding encoding) : IDisposable
{
    readonly Decoder _decoder = encoding.GetDecoder();

    public void Dispose() => stream?.Dispose();

    internal long CountCharacters()
    {
        Span<byte> buffer = stackalloc byte[2048];

        long count = 0;
        var checkPreamble = true;

        int read = 0;
        do
        {
            read = stream.Read(buffer);

            // first Read, require to check UTF8 BOM
            if (checkPreamble)
            {
                int positionBegin = 0;
                if (buffer[..3].SequenceEqual(encoding.Preamble))
                {
                    positionBegin = encoding.Preamble.Length;
                }
                checkPreamble = false;

                count += GetCharCount(buffer[positionBegin..read]);
                continue;
            }

            count += GetCharCount(buffer[..read]);

        } while (read > 0 && read == buffer.Length);

        return count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe int GetCharCount(in ReadOnlySpan<byte> bytes)
    {
        fixed (byte* bytesPtr = &MemoryMarshal.GetReference(bytes))
        {
            return encoding.GetCharCount(bytesPtr, bytes.Length);
        }
    }

    internal unsafe long UnsafeCountCharacters()
    {
        byte[] buffer = ArrayPool<byte>.Shared.Rent(2048);

        long count = 0;
        var checkPreamble = true;

        int read = 0;
        do
        {
            read = stream.Read(buffer, 0, 2048);

            // first Read, require to check UTF8 BOM
            if (checkPreamble)
            {
                int positionBegin = 0;
                if (buffer.AsSpan()[..3].SequenceEqual(encoding.Preamble))
                {
                    positionBegin = encoding.Preamble.Length;
                }
                checkPreamble = false;

                fixed (byte* bytesPtr = buffer.AsSpan()[positionBegin..read])
                {
                    count += encoding.GetCharCount(bytesPtr, read - positionBegin);
                }

                continue;
            }

            fixed (byte* bytesPtr = buffer)
            {
                count += encoding.GetCharCount(bytesPtr, read);
            }


        } while (read > 0 && read == buffer.Length);

        ArrayPool<byte>.Shared.Return(buffer);

        return count;
    }

    // [MethodImpl(MethodImplOptions.AggressiveInlining)] // called directly by GetCharCountCommon
    // private protected sealed override unsafe int GetCharCountFast(byte* pBytes, int bytesLength, DecoderFallback? fallback, out int bytesConsumed)
    // {
    //     // The number of UTF-16 code units will never exceed the number of UTF-8 code units,
    //     // so the addition at the end of this method will not overflow.



    //     byte* ptrToFirstInvalidByte = Utf8Utility.GetPointerToFirstInvalidByte(pBytes, bytesLength, out int utf16CodeUnitCountAdjustment, out _);

    //     int tempBytesConsumed = (int)(ptrToFirstInvalidByte - pBytes);
    //     bytesConsumed = tempBytesConsumed;

    //     return tempBytesConsumed + utf16CodeUnitCountAdjustment;
    // }
}
