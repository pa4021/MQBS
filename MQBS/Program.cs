using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace MQBS
{
    class Program
    {
        static void Main(string[] args)
        {



        }
    }

    public class MessageQueueBufferStream : Stream
    {
        private BlockingCollection<byte[]> queue;

        public MessageQueueBufferStream()
        {
            queue = new BlockingCollection<byte[]>(8);
        }

        public override bool CanRead => true;

        public override bool CanSeek => throw new System.NotImplementedException();

        public override bool CanWrite => true;

        public override long Length => throw new System.NotImplementedException();

        public override long Position { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public override void Flush()
        {

        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!queue.TryTake(out var byteArray, Timeout.Infinite))
                return 0;
            if (offset != 0)
                throw new Exception("offset must be zaro");

            if (!(offset == 0 && count == byteArray.Length))
                throw new Exception("length are not equal.");
            Buffer.BlockCopy(byteArray, 0, buffer, 0, count);

            throw new Exception("salam");

        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var binaryToWrite = new byte[count];
            Buffer.BlockCopy(buffer, offset, binaryToWrite, 0, count);
            queue.Add(binaryToWrite);
        }
    }
}
