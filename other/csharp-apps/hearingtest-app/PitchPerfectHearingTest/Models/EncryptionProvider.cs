using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PitchPerfectHearingTest.Models
{
    public class EncryptionProvider
    {
		private RSACryptoServiceProvider _cryptoProvider;

		public EncryptionProvider()
		{
			_cryptoProvider = new RSACryptoServiceProvider(1024);
			_cryptoProvider.FromXmlString("<RSAKeyValue><Modulus>p4VvQfQ0j6gMC2+nHSwnUbt1so5lJs7sip0FleSRF/lP+3UGqcugCVvfrTjiYlSpjH+/BpT4rqiHWWGJSqTtBpk0TEsuXXR3rGC9EOSLHEtIAqT53ab2XtARznecaEBB/93MTXE16o57cP9KhsMFcXB4Gn+r8ueEhSExC5svoqk=</Modulus><Exponent>AQAB</Exponent><P>wEkgOsqWO6ncdcqBek1tvDhBr65HLoQI3rVHbUguVFDYXrnp+gLwtB5iSYvOIhFgb7APttRJ4BtILkf9+t16dw==</P><Q>3weiqzEbMY8AV/llbgR2uaeEXcbvaSto+EquRPBshPFFfpJWV70I/TLEQsbXyiE0iVbFohMNkuk8brSZrNXz3w==</Q><DP>J+0HLC30k97pT+wEhoidSH/F49ykGxx/Wv75Hc/nDsraopCn7Km/oSbN8cd9vcUt6QL9wFDEJiyECFgafISQcQ==</DP><DQ>oh01s4QFRTexw0Cn5pleB0LppxSUL3j7WwdFtxyfdN6/aepK0om4f/snx3YromQSgQXOsClzQ2c/oySpU4VNNw==</DQ><InverseQ>b9x4D7bAs3Ax0djCbNn6BIXEucHFax8xgUn+g2iSsQEMNIX/jha0VoOyvQ+wvOl7kq3HlQUEfT0+JHgigsF/rg==</InverseQ><D>efKs1zpKuPkAK6MP6DCoPttUBrOZ9vJTR2SfGErXRLh8/PRn8VsGGUoEPjlWs3YmJt0n1kbD8YFFZk+WffwpfqHafb5pcMmlc73uHB48GOUFiHZUXrjETOXTlBW9XV1JkOAEV4k7difKDYA76jT4Mv3QOyLJpNDeDYIwHOh21jE=</D></RSAKeyValue>");
		}

		public string Encrypt(string strText)
		{
			return string.IsNullOrEmpty(strText) ? strText : Convert.ToBase64String(_cryptoProvider.Encrypt(Encoding.UTF8.GetBytes(strText), true));
		}

		public string Decrypt(string strText)
		{
			return string.IsNullOrEmpty(strText) ? strText : Encoding.UTF8.GetString(_cryptoProvider.Decrypt(Convert.FromBase64String(strText), true)).ToString();
		}
	}
}
