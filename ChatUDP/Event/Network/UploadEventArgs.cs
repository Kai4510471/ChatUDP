namespace ChatUDP.Event.Network
{
    public class UploadEventArgs : NetworkEventArgs
    {
        public int UploadLength { get; }

        public UploadEventArgs(int uploadLength)
        {
            UploadLength = uploadLength;
        }
    }
}