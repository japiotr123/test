using PolMedUMG.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolMedUMG.ViewModel
{
    public class SpecialistsViewModel
    {
        public ObservableCollection<Specialist> Specialists { get; set; }

        public SpecialistsViewModel()
        {
            Specialists = new ObservableCollection<Specialist>
            {
                new Specialist { Icon = "Stethoscope", Title = "Lekarz rodzinny", Name = "dr Anna Nowak" },
                new Specialist { Icon = "Heartbeat", Title = "Kardiolog", Name = "dr Piotr Zieliński" },
                new Specialist { Icon = "Shield", Title = "Dermatolog", Name = "dr Marta Kaczmarek" },
                new Specialist { Icon = "Home", Title = "Pediatra", Name = "dr Tomasz Wójcik" },
                new Specialist { Icon = "UserMd", Title = "Dentysta", Name = "dr Agnieszka Pich" },
                new Specialist { Icon = "UserMd", Title = "Ginekolog", Name = "dr Michał Kamiński" },
                new Specialist { Icon = "Bed", Title = "Ortopeda", Name = "dr Katarzyna Woźniak" },
                new Specialist { Icon = "UserMd", Title = "Endokrynolog", Name = "dr Marcin Jankowskio" },
                new Specialist { Icon = "Eye", Title = "Okulista", Name = "dr Barbara Wiśniewska" },
                new Specialist { Icon = "UserMd", Title = "Neurolog", Name = "dr Łukasz Kowalczyk" },
                new Specialist { Icon = "UserMd", Title = "Laryngolog", Name = "dr Ewa Szymańska" },
                new Specialist { Icon = "UserMd", Title = "Reumatolog", Name = "dr Paweł Mak" }
            };
        }
    }
}
