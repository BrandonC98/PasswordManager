using System;

namespace PasswordManagerData
{
    public partial class MasterPassword
    {

        public override string ToString()
        {
            return $"Master Password Id: {Id}   Hash: {Convert.ToBase64String(Hash)}    Salt: {Convert.ToBase64String(Salt)}    Iterations: {Iterations}";

        }

    }
}
