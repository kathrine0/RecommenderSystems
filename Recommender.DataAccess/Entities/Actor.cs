using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recommender.DataAccess.Entities
{

    public partial class Actor
    {
        public Actor()
        {
            Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
