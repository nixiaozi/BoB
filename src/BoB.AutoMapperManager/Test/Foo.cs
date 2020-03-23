using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.AutoMapperManager.Test
{
    public class Foo
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }  //不建议存储年龄如果这个年龄是随着实际年龄增长的话，或者可以通过，消息队列触发

    }
}
