using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QQBatchSend.IR.AuthTool.Common
{
    public class RSACryption
    {
        #region RSA 加密解密  
        #region RSA 的密钥产生
        //public static string privateKey = "<RSAKeyValue><Modulus>z9tWf8djU4cCcahw0PjNCYHpBuWyZ6i6ufY6cRcWweAWa13HwU/AEKMPI7S7ZhsdubwkQShNAcpvEQP1fH2ZiKZGpUdzirfRV0Nd7TaSSFvMADyAwnanKcJp5gKAyxXX5VIIT+b6ZAE3sxQp7AUkpuJuTcX0dTI08uRXjpBm9ik=</Modulus><Exponent>AQAB</Exponent><P>2tawgBOCGQivqznVrIA3JbdM9wp9kBIHOwP3DmGydx158KKBsdphsNPlr/WYNCYeRtwwCt93rO9kDEQGVnJR5w==</P><Q>8yc/d+MXUH/rcmzXqxAqrlxe5yZFAkCvEqkgkeNp1Ryw3ejDyqqnipPbSFxWML6tlPpDMWKrkxpvmojJ6kaVbw==</Q><DP>B6j4KKOGLEYnkADIP++E/qb55LPTTVW0NxaecTxuOMkpWYj2rTkNrljcc1pPZ5Pl/2j5rYfA8qa2g89lwWTjBQ==</DP><DQ>vW//h7TabjIrSou3/yNqTUvT+HydWSLyAzTXFjI6OvnOZiU5nUUVoOaO+jJOSE3WSPIttWUtTT9UYe5eeXKiaw==</DQ><InverseQ>ZItUxCMS9hfUDuXeTHdOHLflQ10UupEBl9e5c4P4odZQCSKW7OhaySOvZSzxEydHQWvuCariv4C9tgNWv78O+g==</InverseQ><D>oSYSR/rT3RxdHrVS0hEzFd3sRnaq/IbqTebCpI8cC6+qcg9BIXStGbjxseMbLyxyvV9KVGHkYjEKLwz8tEnET7E7naZE3eitp4EuM4y4MnyiwnZkW6Ywe1rUTsYGM5sIeGQc+4hR99BJShGgQXiOSUcxUDNVVwcDu7LA8I9iCQk=</D></RSAKeyValue>";
        public static string publicKey = "<RSAKeyValue><Modulus>z9tWf8djU4cCcahw0PjNCYHpBuWyZ6i6ufY6cRcWweAWa13HwU/AEKMPI7S7ZhsdubwkQShNAcpvEQP1fH2ZiKZGpUdzirfRV0Nd7TaSSFvMADyAwnanKcJp5gKAyxXX5VIIT+b6ZAE3sxQp7AUkpuJuTcX0dTI08uRXjpBm9ik=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


        /// <summary>  
        /// RSA产生密钥  
        /// </summary>  
        /// <param name="xmlKeys">私钥</param>  
        /// <param name="xmlPublicKey">公钥</param>  
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA加密函数  
        //##############################################################################   
        //RSA 方式加密   
        //KEY必须是XML的形式,返回的是字符串   
        //该加密方式有长度限制的！  
        //##############################################################################   

        /// <summary>  
        /// RSA的加密函数  
        /// </summary>  
        /// <param name="xmlPublicKey">公钥</param>  
        /// <param name="encryptString">待加密的字符串</param>  
        /// <returns></returns>  
        public string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA的加密函数   
        /// </summary>  
        /// <param name="xmlPublicKey">公钥</param>  
        /// <param name="EncryptString">待加密的字节数组</param>  
        /// <returns></returns>  
        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {
            try
            {
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                CypherTextBArray = rsa.Encrypt(EncryptString, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的解密函数          
        /// <summary>  
        /// RSA的解密函数  
        /// </summary>  
        /// <param name="xmlPrivateKey">私钥</param>  
        /// <param name="decryptString">待解密的字符串</param>  
        /// <returns></returns>  
        public string RSADecrypt(string xmlPrivateKey, string decryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA的解密函数   
        /// </summary>  
        /// <param name="xmlPrivateKey">私钥</param>  
        /// <param name="DecryptString">待解密的字节数组</param>  
        /// <returns></returns>  
        public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                DypherTextBArray = rsa.Decrypt(DecryptString, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region RSA数字签名  
        #region 获取Hash描述表          
        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="strSource">待签名的字符串</param>  
        /// <param name="HashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(string strSource, ref byte[] HashData)
        {
            try
            {
                byte[] Buffer;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="strSource">待签名的字符串</param>  
        /// <param name="strHashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                //从字符串中取得Hash描述   
                byte[] Buffer;
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="objFile">待签名的文件</param>  
        /// <param name="HashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //从文件中取得Hash描述   
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// 获取Hash描述表  
        /// </summary>  
        /// <param name="objFile">待签名的文件</param>  
        /// <param name="strHashData">Hash描述</param>  
        /// <returns></returns>  
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //从文件中取得Hash描述   
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名  
        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="HashbyteSignature">待签名Hash描述</param>  
        /// <param name="EncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="HashbyteSignature">待签名Hash描述</param>  
        /// <param name="m_strEncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] EncryptedSignatureData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="strHashbyteSignature">待签名Hash描述</param>  
        /// <param name="EncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;

                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();


                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// RSA签名  
        /// </summary>  
        /// <param name="strKeyPrivate">私钥</param>  
        /// <param name="strHashbyteSignature">待签名Hash描述</param>  
        /// <param name="strEncryptedSignatureData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5   
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名   
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA 签名验证  
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="HashbyteDeformatter">Hash描述</param>  
        /// <param name="DeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="strHashbyteDeformatter">Hash描述</param>  
        /// <param name="DeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="HashbyteDeformatter">Hash描述</param>  
        /// <param name="strDeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>  
        /// RSA签名验证  
        /// </summary>  
        /// <param name="strKeyPublic">公钥</param>  
        /// <param name="strHashbyteDeformatter">Hash描述</param>  
        /// <param name="strDeformatterData">签名后的结果</param>  
        /// <returns></returns>  
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new

    System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5   
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}
