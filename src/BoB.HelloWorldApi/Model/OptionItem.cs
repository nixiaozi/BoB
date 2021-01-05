using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoB.HelloWorldApi.Model
{
    public class OptionItem<K,V> where K:struct 
    {
        public K key { get; set; }

        public V value { get; set; }
    }
}
