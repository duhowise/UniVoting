using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniVoting.WPF
{
    public class Item
    {
        public int PictureID { get; set; }
        public string Name { get; set; }
        public string PictureString
        {
            get { return "../Resources/images/people_on_the_beach_300x300.jpg"; }
        }
    }
}
