using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using PS_8.Models;


namespace PS_8.DAL
{ 
    public class ProductXmlDB : IProductDB
    {
        private int GetNextID()
        {
            List<Product> products = List();
            return products[products.Count-1].id + 1;
        }
        public Product Get(int _id)
        {
            XmlNode node = XmlNodeProductGet(_id);
            return XmlNodeProduct2Product(node);
        }
        public void Update(Product _product)
        {
            XmlNode node = XmlNodeProductGet(_product.id);
            node["name"].InnerText = _product.name;
            node["price"].InnerText = _product.price.ToString();
            SaveXmlBase();
        }
        public void Delete(int _id)
        {
            XmlNode toDel = db.SelectSingleNode("/store/product[@id=" + _id.ToString() + "]");
            if (toDel != null)
            {
                toDel.ParentNode.RemoveChild(toDel);
                SaveXmlBase();
            }
        }
        public void Add(Product _product)
        {
            _product.id = GetNextID();
            XmlNode node = db.DocumentElement;
            XmlElement element = db.CreateElement("product");
            XmlElement name = db.CreateElement("name");
            XmlElement price = db.CreateElement("price");
            name.InnerText = _product.name;
            price.InnerText = _product.price.ToString();
            XmlAttribute id = db.CreateAttribute("id");
            id.Value = _product.id.ToString();
            element.Attributes.Append(id);
            element.AppendChild(name);
            element.AppendChild(price);
            node.InsertAfter(element, node.LastChild);
            SaveXmlBase();
        }

        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        public ProductXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path");
            db.Load(xmlDB_path);
            
        }
        public List<Product> List()
        {
            List<Product> productList = new List<Product>();
            XmlNodeList productXmlNodeList = db.SelectNodes("/store/product");

            foreach (XmlNode productXmlNode in productXmlNodeList)
            {
                productList.Add(XmlNodeProduct2Product(productXmlNode));
            }
            return productList;
        }
        private Product XmlNodeProduct2Product(XmlNode node)
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }

        public Product ProductGet(int id)
        {
            Product p = new Product();
            OpenXmlBase();
            XmlNode node = XmlNodeProductGet(id);
            return XmlNodeProduct2Product(node);
        }
        private void OpenXmlBase()
        {
            db.Load("DATA/store.xml");
        }
        private void SaveXmlBase()
        {
            db.Save("DATA/store.xml");
        }
        private XmlNode XmlNodeProductGet(int id)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/product[@id=" + id.ToString() +
           "]");
            node = list[0];
            return node;
        }

    }
}
