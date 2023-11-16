using System.DirectoryServices.Protocols;
using System.DirectoryServices.AccountManagement;

namespace LDAPAuthentication
{
    public class LDAPAuthenticator
    {
        private string _ldapServer;
        private int _ldapPort;

        public LDAPAuthenticator(string ldapServer, int ldapPort)
        {
            _ldapServer = ldapServer;
            _ldapPort = ldapPort;
        }

        public bool Authenticate(string username, string password)
        {
            try
            {
                LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(_ldapServer, _ldapPort);
                using (LdapConnection Connection = new LdapConnection(identifier))
                {
                    Connection.AuthType = AuthType.Basic;
                    Connection.Credential = new System.Net.NetworkCredential(username, password);
                    Connection.Bind();
                    return true;
                }
            }
            catch(LdapException ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        public static void TestAuthentication()
        {
            var ldapAuthenticator = new LDAPAuthenticator("server", 389);//replace with your server and port
            var isAuthenticated = ldapAuthenticator.Authenticate("username", "password");//replace with your username and password
            Console.WriteLine(isAuthenticated ? "Authenticated" : "Not Authenticated");
        }
    }
            //if that doesnt work try this
    //class LdapAuthenticationService
    //{
    //    public enum AuthenticationResult
    //    {
    //        Success,
    //        InvalidCredentials,
    //        UnknownError
    //    }
    //    public AuthenticationResult Authentication(string username, string password)
    //    {
    //        try
    //        {
    //            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
    //            {
    //                bool isValid = context.ValidateCredentials(username, password);
    //                if (isValid)
    //                {
    //                    return AuthenticationResult.Success;
    //                }
    //                else
    //                {
    //                    return AuthenticationResult.InvalidCredentials;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine("Error during LDAP Authentication" + ex.Message);
    //            return AuthenticationResult.UnknownError;
    //        }
    //    }
    //}
}