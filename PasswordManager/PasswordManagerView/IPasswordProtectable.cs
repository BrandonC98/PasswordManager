using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerView
{
    public interface IPasswordProtectable
    {

        public void OnPasswordConfirmation(byte[] hashKey);

    }
}
