using System;
using System.Linq;
namespace ConsoleApp1
{
    class Person
    {
        public string name;
        public int age;
        public int weight;
        public string gender;
        public Person(string Iname, int Iage, int Iweight, string Igender)
        {
            name = Iname;
            age = Iage;
            weight = Iweight;
            gender = Igender;
        }
    }


    class Node
    {
        public Node parent;
        public Node right;
        public Node left;
        public int key;
        public Person person;
        public bool deleted = false;
        public Node(int nodeKey,Person Iperson=null, Node nodeParent = null, Node nodeRight = null, Node nodeLeft = null)
        {
            person = Iperson;
            key = nodeKey;
            parent = nodeParent;
            right = nodeRight;
            left = nodeLeft;
        }
    }

    class BST
    {
        public Person[] temp;
        public Node root;
        public int counter = 0;
        public BST(Node treeRoot=null)
        {
            root = treeRoot;
            temp = new Person[1000];
        }

        public int insertNode(Node newNode2)
        {
            int newKey = newNode2.key;
            Node thisNode = root;
            while (true)
            {
                if (thisNode.key > newKey)
                {
                    if (thisNode.left == null)
                    {
                        thisNode.left = newNode2;
                        newNode2.parent = thisNode;
                        return newKey;
                    }
                    thisNode = thisNode.left;
                }
                else
                {
                    if (thisNode.right == null)
                    {
                        thisNode.right = newNode2;
                        newNode2.parent = thisNode;
                        return newKey;
                    }
                    thisNode = thisNode.right;
                }
            }
        }


        public bool search(int input)
        {
            Console.WriteLine("search started");
            counter = 0;
            Node thisNode = root;
            while (thisNode != null)
            {
                if (thisNode.deleted)
                {
                    if (input >= thisNode.key)
                    {
                        thisNode = thisNode.right;
                    }
                    else if(input < thisNode.key)
                    {
                        thisNode = thisNode.left;
                    }
                    continue;
                }
                if (input == thisNode.key)
                {
                    temp[counter] = thisNode.person;
                    counter++;
                    if (thisNode.right != null && thisNode.right.key == thisNode.key)
                    {
                        continue;
                    }
                    return true;
                }
                else if (input > thisNode.key)
                {
                    thisNode = thisNode.right;
                }
                else
                {
                    thisNode = thisNode.left;
                }
            }
            return false;
        }

        public Node searchAndGetNode(int input)
        {
            Node thisNode = root;
            while (thisNode != null)
            {
                if (thisNode.deleted)
                {
                    if (input >= thisNode.key)
                    {
                        thisNode = thisNode.right;
                    }
                    else if (input < thisNode.key)
                    {
                        thisNode = thisNode.left;
                    }
                    continue;
                }
                if (input == thisNode.key)
                {
                    return thisNode;
                }
                else if (input > thisNode.key)
                {
                    thisNode = thisNode.right;
                }
                else
                {
                    thisNode = thisNode.left;
                }
            }
            return null;
        }

        public Node getMinNodeCustomRoot(Node thisNode)
        {
            while (thisNode.left != null)
            {
                thisNode = thisNode.left;
            }
            return thisNode;
        }
        public Node getMaxNodeCustomRoot(Node thisNode)
        {
            while (thisNode.right != null)
            {
                thisNode = thisNode.right;
            }
            return thisNode;
        }

        public Node getsuccessor(Node main)
        {
            return getMinNodeCustomRoot(main.right);
        }
        public Node getpredess(Node main)
        {
            return getMaxNodeCustomRoot(main.left);
        }
        public bool delete(int input)
        {
            Node targetNode = searchAndGetNode(input);
            if (targetNode == null)
            {
                return false;
            }

            if (targetNode.right == null && targetNode.left == null)
            {
                if (targetNode.key >= targetNode.parent.key)
                {
                    targetNode.parent.right = null;
                }
                else
                {
                    targetNode.parent.left = null;
                }
                targetNode.parent = null;
            }
            else if (targetNode.right != null && targetNode.left == null)
            {
                targetNode.right.parent = targetNode.parent;
                if (targetNode == root)
                {
                    root = targetNode.right;
                    targetNode.right.parent = null;
                    return true;
                }
                if (targetNode.key < targetNode.parent.key)
                {
                    targetNode.parent.left = targetNode.right;
                }
                else
                {
                    targetNode.parent.right = targetNode.right;
                }


            }
            else if (targetNode.right == null && targetNode.left != null)
            {
                targetNode.left.parent = targetNode.parent;
                if (targetNode == root)
                {
                    root = targetNode.left;
                    targetNode.left.parent = null;
                    return true;
                }
                if (targetNode.key < targetNode.parent.key)
                {
                    targetNode.parent.left = targetNode.left;
                }
                else
                {
                    targetNode.parent.right = targetNode.left;
                }
            }
            else if (targetNode.right != null && targetNode.left != null)
            {
                Node succ = getpredess(targetNode);
                delete(succ.key);
                targetNode.key = succ.key;
            }
            return true;
        }
    }
    class MegaTree
    {
        public BST F_age;
        public BST F_weight;
        public BST F_name;

        public BST M_age;
        public BST M_weight;
        public BST M_name;

        public MegaTree()
        {
            Person NewPerson = new Person("-",0,0,"-");
            Node newNode = new Node(-1, NewPerson);
            F_age = new BST(newNode);
            F_weight = new BST(newNode);
            F_name = new BST(newNode);
            M_age = new BST(newNode);
            M_weight = new BST(newNode);
            M_name = new BST(newNode);
        }
        static int getASCII(string text)
        {
            int sum = 0;
            foreach(char ch in text)
            {
                sum = sum + (int)ch;
            }
            return sum;
        }

        public Person insert(string name,int age, int weight, string gender)
        {
            Person newPerson = new Person(name, age, weight, gender);
            if (gender == "M")
            {
                Node newNode = new Node(newPerson.age, newPerson);
                M_age.insertNode(newNode);
                Node newNode2 = new Node((int)newPerson.weight, newPerson);
                M_weight.insertNode(newNode2);
                Node newNode3 = new Node(getASCII(newPerson.name), newPerson);
                M_name.insertNode(newNode3);
            }
            else
            {
                Node newNode = new Node(newPerson.age, newPerson);
                F_age.insertNode(newNode);
                Node newNode2 = new Node((int)newPerson.weight, newPerson);
                F_weight.insertNode(newNode2);
                Node newNode3 = new Node(getASCII(newPerson.name), newPerson);
                F_name.insertNode(newNode3);
            }
            return newPerson;
        }

        public void search(string name, string age, string weight, string gender)
        {
            M_name.counter = 0;
            M_age.counter = 0;
            M_weight.counter = 0;
            F_name.counter = 0;
            F_age.counter = 0;
            F_weight.counter = 0;

            if (gender == "M")
            {
                if (name != "null")
                {
                    M_name.search(getASCII(name));
                }
                else
                {
                    name = null;
                }

                if (age != "null")
                {
                    M_age.search(Convert.ToInt32(age));
                }
                else
                {
                    age = null;
                }

                if (weight != "null")
                {
                    M_weight.search(Convert.ToInt32(weight));
                }
                else
                {
                    weight = null;
                }


                if (age == null)
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            Console.WriteLine("=|");
                        }
                        else
                        {
                            for (int i = 0; i < M_name.counter; i++)
                            {
                                Person one = M_name.temp[i];
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            for (int i = 0; i < M_weight.counter; i++)
                            {
                                Person one = M_weight.temp[i];
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            for(int i = 0; i < M_name.counter; i++)
                            {
                                Person one = M_name.temp[i];
                                for(int j = 0; j < M_weight.counter; j++)
                                {
                                    Person two = M_weight.temp[i];
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                    Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            for (int i = 0; i < M_age.counter; i++)
                            {
                                Person one = M_age.temp[i];
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < M_name.counter; i++)
                            {
                                Person one = M_name.temp[i];
                                for (int j = 0; j < M_age.counter; j++)
                                {
                                    Person two = M_age.temp[i];
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            for (int i = 0; i < M_age.counter; i++)
                            {
                                Person one = M_age.temp[i];
                                for (int j = 0; j < M_weight.counter; j++)
                                {
                                    Person two = M_weight.temp[i];
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < M_age.counter; i++)
                            {
                                Person one = M_age.temp[i];
                                for (int j = 0; j < M_weight.counter; j++)
                                {
                                    Person two = M_weight.temp[i];
                                    for (int k = 0; k < M_name.counter; k++)
                                    {
                                        Person three = M_age.temp[i];
                                        if (one.name == two.name && one.age == two.age && one.weight == two.weight &&   three.name == two.name && three.age == two.age && three.weight == two.weight)
                                            Console.Write(one.name);
                                            Console.Write(" ");
                                            Console.Write(one.age);
                                            Console.Write(" ");
                                            Console.Write(one.weight);
                                            Console.Write(" ");
                                            Console.Write(one.gender);
                                            Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if(gender == "F")
            {
                if (name != "null")
                {
                    F_name.search(getASCII(name));
                }
                else
                {
                    name = null;
                }

                if (age != "null")
                {
                    F_age.search(Convert.ToInt32(age));
                }
                else
                {
                    age = null;
                }

                if (weight != "null")
                {
                    F_weight.search(Convert.ToInt32(weight));
                }
                else
                {
                    weight = null;
                }
                if (age == null)
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            Console.WriteLine("=|");
                        }
                        else
                        {
                            for (int i = 0; i < F_name.counter; i++)
                            {
                                Person one = F_name.temp[i];
                                Console.WriteLine(F_name.temp.Length);
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            for (int i = 0; i < F_weight.counter; i++)
                            {
                                Person one = F_weight.temp[i];
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in F_name.temp)
                            {
                                foreach (Person two in F_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            foreach (Person one in F_age.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in F_age.temp)
                            {
                                foreach (Person two in F_name.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            foreach (Person one in F_age.temp)
                            {
                                foreach (Person two in F_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < F_age.counter; i++)
                            {
                                Person one = F_age.temp[i];
                                for (int j = 0; j < F_weight.counter; j++)
                                {
                                    Person two = F_weight.temp[i];
                                    for (int k = 0; k < F_name.counter; k++)
                                    {
                                        Person three = F_age.temp[i];
                                        if (one.name == two.name && one.age == two.age && one.weight == two.weight && three.name == two.name && three.age == two.age && three.weight == two.weight)
                                            Console.Write(one.name);
                                            Console.Write(" ");
                                            Console.Write(one.age);
                                            Console.Write(" ");
                                            Console.Write(one.weight);
                                            Console.Write(" ");
                                            Console.Write(one.gender);
                                            Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (name != "null")
                {
                    F_name.search(getASCII(name));
                    M_name.search(getASCII(name));
                }
                else
                {
                    name = null;
                }

                if (age != "null")
                {
                    F_age.search(Convert.ToInt32(age));
                    M_age.search(Convert.ToInt32(age));
                }
                else
                {
                    age = null;
                }

                if (weight != "null")
                {
                    F_weight.search(Convert.ToInt32(weight));
                    M_weight.search(Convert.ToInt32(weight));
                }
                else
                {
                    weight = null;
                }

                if (age == null)
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            Console.WriteLine("=|");
                        }
                        else
                        {
                            foreach (Person one in M_name.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            foreach (Person one in M_weight.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in M_name.temp)
                            {
                                foreach (Person two in M_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            foreach (Person one in M_age.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in M_age.temp)
                            {
                                foreach (Person two in M_name.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            foreach (Person one in M_age.temp)
                            {
                                foreach (Person two in M_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            foreach (Person one in M_age.temp)
                            {
                                foreach (Person two in M_weight.temp)
                                {
                                    foreach (Person three in M_weight.temp)
                                    {
                                        if (one.name == two.name && one.age == two.age && one.weight == two.weight && three.name == two.name && three.age == two.age && three.weight == two.weight)
                                            Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
                if (age == null)
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            Console.WriteLine("=|");
                        }
                        else
                        {
                            foreach (Person one in F_name.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            foreach (Person one in F_weight.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in F_name.temp)
                            {
                                foreach (Person two in F_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (weight == null)
                    {
                        if (name == null)
                        {
                            foreach (Person one in F_age.temp)
                            {
                                Console.Write(one.name);
                                Console.Write(" ");
                                Console.Write(one.age);
                                Console.Write(" ");
                                Console.Write(one.weight);
                                Console.Write(" ");
                                Console.Write(one.gender);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            foreach (Person one in F_age.temp)
                            {
                                foreach (Person two in F_name.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (name == null)
                        {
                            foreach (Person one in F_age.temp)
                            {
                                foreach (Person two in F_weight.temp)
                                {
                                    if (one.name == two.name && one.age == two.age && one.weight == two.weight)
                                        Console.Write(one.name);
                                    Console.Write(" ");
                                    Console.Write(one.age);
                                    Console.Write(" ");
                                    Console.Write(one.weight);
                                    Console.Write(" ");
                                    Console.Write(one.gender);
                                    Console.WriteLine();
                                }
                            }
                        }
                        else
                        {
                            foreach (Person one in F_age.temp)
                            {
                                foreach (Person two in F_weight.temp)
                                {
                                    foreach (Person three in F_weight.temp)
                                    {
                                        if (one.name == two.name && one.age == two.age && one.weight == two.weight && three.name == two.name && three.age == two.age && three.weight == two.weight)
                                            Console.Write(one.name);
                                        Console.Write(" ");
                                        Console.Write(one.age);
                                        Console.Write(" ");
                                        Console.Write(one.weight);
                                        Console.Write(" ");
                                        Console.Write(one.gender);
                                        Console.WriteLine();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
    class Program
    {
        public static void Main(String[] args)
        {
            MegaTree megatree = new MegaTree();
            int nodeCount = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < nodeCount; i++)
            {
                string[] parm = Console.ReadLine().Split();
                megatree.insert(parm[0], Convert.ToInt32(parm[1]), Convert.ToInt32(parm[2]), parm[3]);
            }
            int operationCount = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < operationCount; i++)
            {
                string[] parm2 = Console.ReadLine().Split();
                if (parm2[0] == "delete")
                {
                    Console.Write("NOT IMPLEMENTED");
                }else if (parm2[0] == "insert")
                {
                    Person newpr = megatree.insert(parm2[1], Convert.ToInt32(parm2[2]), Convert.ToInt32(parm2[3]), parm2[4]);
                    Console.Write(newpr.name);
                    Console.Write(" ");
                    Console.Write(newpr.age);
                    Console.Write(" ");
                    Console.Write(newpr.weight);
                    Console.Write(" ");
                    Console.Write(newpr.gender);
                    Console.WriteLine();
                }
                else if (parm2[0] == "search")
                {
                   megatree.search(parm2[1], (parm2[2]), (parm2[3]), parm2[4]);
                }
            }
        }
    }
}
