using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Сущность \"{name}\" с ключом ({key}) не найдена.")
        {
        }

        public NotFoundException(string name, IEnumerable<object> keys)
            : base($"Сущность \"{name}\" с ключами ({string.Join(", ", keys)}) не найдена.")
        {
        }
    }
}
