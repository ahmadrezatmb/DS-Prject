using System;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Disease
    {
        public string name;
        public List<Drug> better_with;
        public List<Drug> worse_with;
        public Disease(string name)
        {
            this.name = name;
            this.better_with = new List<Drug>();
            this.worse_with = new List<Drug>();
        }

        public void add_better_with(Drug drug)
        {
            better_with.Add(drug);
        }

        public void add_worse_with(Drug drug)
        {
            worse_with.Add(drug);
        }
    }

    class Drug
    {
        public string name;
        public int price;
        public List<Disease> good_for;
        public List<Disease> bad_for;
        public Drug(string name, int price)
        {
            this.name = name;
            this.price = price;
            this.good_for = new List<Disease>();
            this.bad_for = new List<Disease>();
        }

        public void add_good_for(Disease disease)
        {
            this.good_for.Add(disease);
        }

        public void add_bad_for(Disease disease)
        {
            this.bad_for.Add(disease);
        }
    }

    class Effect
    {
        public string name;
        public Drug first_drug;
        public Drug sec_drug;
        public Effect(string name, Drug first_drug, Drug sec_drug)
        {
            this.name = name;
            this.first_drug = first_drug;
            this.sec_drug = sec_drug;
        }
    }

    class EffectDataBase
    {
        public Effect[] all_effects;
        public EffectDataBase()
        {
            this.all_effects = new Effect[10000000]; // TODO: change the number later
        }
        private int hash_by_first_drug_name(string name)
        {
            int hash_value = 0;
            foreach (char letter in name)
            {
                hash_value += (int)letter;
            }
            return hash_value;
        }

        public void create(string name, Drug one, Drug two)
        {
            Effect new_effect = new Effect(name, one, two);
            int hash_index = hash_by_name(one.name);
            while (all_effects[hash_index] != null) { hash_index++; }
            all_effects[hash_index] = new_effect;
        }

        public void delete(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_effects[hash_index].name != name) { hash_index++; }
            all_effects[hash_index] = null;
        }

        public Effect read(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_effects[hash_index].name != name) { hash_index++; }
            return all_effects[hash_index];
        }

        public List<Effect> get_effects_of_other_drugs(string drug_name)
        {
            List<Effect> output = new List<Effect>();
            int hash_index = hash_by_first_drug_name(drug_name);
            while (all_effects[hash_index].name == drug_name)
            {
                output.Add(all_effects[hash_index]);
                hash_index++;
            }
            return output;
        }
    }

    class Drugdatabase
    {
        public Drug[] all_drugs;
        public Drugdatabase()
        {
            this.all_drugs = new Drug[100000000];
        }
        private int hash_by_name(string name)
        {
            int hash_value = 0;
            foreach (char letter in name)
            {
                hash_value += (int)letter;
            }
            return hash_value;
        }
        public void create(string name, int price)
        {
            Drug new_drug = new Drug(name, price);
            int hash_index = hash_by_name(name);
            while (all_drugs[hash_index] != null) { hash_index++; }
            all_drugs[hash_index] = new_drug;
        }
        public void delete(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_drugs[hash_index].name != name) { hash_index++; }
            all_drugs[hash_index] = null;
        }
        public Drug read(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_drugs[hash_index].name != name) { hash_index++; }
            return all_drugs[hash_index];
        }
    }

    class DiseaseDataBase
    {
        public Disease[] all_disease;
        public DiseaseDataBase()
        {
            this.all_disease = new Disease[100000000];
        }
        private int hash_by_name(string name)
        {
            int hash_value = 0;
            foreach (char letter in name)
            {
                hash_value += (int)letter;
            }
            return hash_value;
        }
        public void create(string name, int price)
        {
            Disease new_disease = new Disease(name);
            int hash_index = hash_by_name(name);
            while (all_disease[hash_index] != null) { hash_index++; }
            all_disease[hash_index] = new_disease;
        }
        public void delete(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_disease[hash_index].name != name) { hash_index++; }
            all_disease[hash_index] = null;
        }
        public Disease read(string name)
        {
            int hash_index = hash_by_name(name);
            while (all_disease[hash_index].name != name) { hash_index++; }
            return all_disease[hash_index];
        }
    }
    class Program
    {
        public static void Main(String[] args)
        {
            DiseaseDataBase DiseaseDataBase = new DiseaseDataBase();
            Drugdatabase DrugDB = new Drugdatabase();
            EffectDataBase EffectDB = new EffectDataBase();
            Console.WriteLine("heh");
        }
    }
}
