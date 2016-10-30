using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;

namespace BioPass {
    class DPAPI {
        private byte[] entropy = { 0, 8, 1, 6, 1, 9, 9, 4 };

        public byte[] protect(String data) {
            try { 
                byte[] plainTextBytes = Encoding.Unicode.GetBytes( data );
                byte[] encrypted = ProtectedData.Protect(
                    plainTextBytes, entropy, DataProtectionScope.CurrentUser
                );
                return encrypted;
            } catch (CryptographicException e) {
                Console.WriteLine("Something broke. Data not encrypted: " + e.ToString());
                return null;
            }
        }
        
        public String unprotect(byte[] data) {
            try { 
                byte[] decrypted = ProtectedData.Unprotect(
                     data, entropy, DataProtectionScope.CurrentUser
                );
                String plaintext = Encoding.Unicode.GetString(decrypted, 0, decrypted.Length);
                return plaintext;
            } catch (CryptographicException e) {
                Console.WriteLine("Something broke. Data not decrypted: " + e.ToString());
                return null;
            }
        }
    }
}
