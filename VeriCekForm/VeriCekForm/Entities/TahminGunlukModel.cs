using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriCekForm.Entities
{
    class TahminGunlukModel
    {

        [BsonElement("GenelDurum")]
        public string GenelDurum { get; set; }



    
    }
}
