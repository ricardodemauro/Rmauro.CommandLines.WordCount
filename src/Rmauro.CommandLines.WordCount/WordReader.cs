using System.Buffers;
using System.Text;

namespace Rmauro.CommandLines.WordCount;

public class WordReader(Stream stream, Encoding encoding) : IDisposable
{
    readonly Decoder _decoder = encoding.GetDecoder();

    public void Dispose() => stream?.Dispose();

    internal long CountCharacters()
    {
        var rentedBuffer = ArrayPool<byte>.Shared.Rent(4096);
        Span<byte> buffer = rentedBuffer.AsSpan();

        long count = 0;

        try
        {
            int read = 0;
            do
            {
                read = stream.Read(buffer);

                count += _decoder.GetCharCount(buffer[..read], false);

            } while (read > 0 && read == buffer.Length);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rentedBuffer);
        }
        return count;
    }
}
