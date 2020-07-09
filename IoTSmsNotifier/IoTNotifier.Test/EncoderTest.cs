using IoTSmsNotifier.Core.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IoTNotifier.Test
{
    [TestClass]
    public class EncoderTest
    {
        [TestMethod]
        public void Base64()
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes("adrianna.majstrak@gmail.com:aac3bfd2");
            Console.WriteLine(System.Convert.ToBase64String(plainTextBytes));
        }
    }
}
