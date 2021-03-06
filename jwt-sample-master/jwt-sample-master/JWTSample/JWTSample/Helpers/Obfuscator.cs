using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;


/// <summary>
/// Summary description for obfuscator
/// </summary>
namespace obfuscate
{
    public class Obfuscator : Stream
    {

        Stream responseStream;
        private IScrambler iScrambler;
        long position;
        StringBuilder responseHtml;


        public Obfuscator(Stream inputStream)
        {
            responseStream = inputStream;
            responseHtml = new StringBuilder();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Close()
        {
            responseStream.Close();
        }

        public override void Flush()
        {
            responseStream.Flush();
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get { return position; }
            set { position = value; }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return responseStream.Seek(offset, origin);
        }

        public override void SetLength(long length)
        {
            responseStream.SetLength(length);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return responseStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string strBuffer = System.Text.UTF8Encoding.UTF8.GetString(buffer, offset, count);

            // ---------------------------------
            // Wait for the closing </html> tag
            // ---------------------------------
            Regex eof = new Regex("</html>", RegexOptions.IgnoreCase);

            if (!eof.IsMatch(strBuffer))
            {
                responseHtml.Append(strBuffer);
            }
            else
            {

                responseHtml.Append(strBuffer);
               // char rtt = '•';
                string finalHtml = responseHtml.ToString();

                iScrambler = new PairwiseScrambler();
                finalHtml = iScrambler.encode(finalHtml.ToCharArray());

                byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(finalHtml);
                responseStream.Write(data, 0, data.Length);

            }
        }

    }
}
