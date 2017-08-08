using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KWGene.Models
{
    public class Node
    {
        public Node() { }
        public Node(int id, string str, string link, string tag, List<Node> node)
        {
            nodeId = id;
            text = str;
            href = link;
            tags = tag;
            nodes = node;
        }
        public int nodeId; //树的节点Id，区别于数据库中保存的数据Id。若要存储数据库数据的Id，添加新的Id属性；若想为节点设置路径，类中添加Path属性
        public string text; //节点名称
        public string href; //节点名称
        public string tags; //节点名称
        public List<Node> nodes; //子节点，可以用递归的方法读取，方法在下一章会总结
    }

    public class state
    {
        bool @checked { get; set; }
    }
}