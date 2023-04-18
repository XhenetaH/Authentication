using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Domain.Entities
{
    public class AuditColumns
    {
        public int InsertBy { get; set; }

        public DateTime InsertDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public int? LUB { get; set; }

        public int LUN { get; set; } = 0;

        private DateTime? _LUD;

        public DateTime? LUD
        {
            get
            {
                return _LUD;
            }
            set
            {
                if (LUN == 0)
                    _LUD = DateTime.MinValue;
                else
                    _LUD = value;
            }
        }
    }
}
