﻿using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class WhatYouLearnEntity
    {
        [Key]
        public int Id { get; set; }

        public List<WhatYouLearnItemsEntity> whatYouLearnItems { get; set; } = new List<WhatYouLearnItemsEntity>();

    }
}
