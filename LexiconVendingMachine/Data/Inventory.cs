using LexiconVendingMachine.Model;
using System;
using System.Collections.Generic;

namespace LexiconVendingMachine.Data
{
	public class Inventory
	{
		// Keep track of products in inventory
		private readonly List<Product> products;

		// How many products are the in the inventory
		public int Size { get => products.Count; }

		// Create a new, empty inventory.
		public Inventory()
		{
			products = new List<Product>();
		}

		// Return a list of all products in the inventory
		public Product[] GetAll()
		{
			return products.ToArray();
		}

		// Add new products to the inventory
		public void AddProduct(Product productToAdd)
		{
			// We can not add nothing to the inventory.
			if(productToAdd is null)
				throw new ArgumentNullException("You can not add null to the inventory!");

			products.Add(productToAdd);
		}

		// Remove one of the products from the inventory
		// Returns true if the product was removed. 
		// Returns false if the product was not found or could not be removed.
		public bool RemoveProduct(Product productToRemove)
		{
			return products.Remove(productToRemove);
		}

		// Check if a certain prodoct exists in the inventory
		// Returns true if it does and false if not.
		public bool ProductExists(Product productToFind)
		{
			return products.Exists(product => product.Equals(productToFind));
		}
	}
}
