using LINQ.DataSources;
using LINQ.Models;

namespace LINQAssSolution2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var top3 = Source.ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
            foreach (var p in top3)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice:F2}");

            // =====

            int page = 2;
            int pageSize = 5;
            var pageProducts = Source.ProductList
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            foreach (var p in pageProducts)
                Console.WriteLine($"{p.ProductID} - {p.ProductName} - {p.UnitPrice:F2}");

            // =====

            var cheapProducts = Source.ProductList
                .OrderBy(p => p.UnitPrice)
                .TakeWhile(p => p.UnitPrice < 25);
            foreach (var p in cheapProducts)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice:F2}");

            // =====

            bool allSeafoodInStock = Source.ProductList
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);
            Console.WriteLine(allSeafoodInStock);

            // =====

            int[] ids = { 3, 9, 13, 18 };
            bool containsNine = ids.Contains(9);
            Console.WriteLine(containsNine);

            // =====

            var productGroups = Source.ProductList.GroupBy(p => p.Category);
            foreach (var g in productGroups)
                Console.WriteLine($"{g.Key}: {g.Count()} products");

            // =====

            var namesByCategory = Source.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Names = g.Select(p => p.ProductName) });
            foreach (var item in namesByCategory)
            {
                Console.WriteLine(item.Category);
                foreach (var name in item.Names)
                    Console.WriteLine($"  {name}");
            }

            // =====

            var bigCategories = Source.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3);
            foreach (var g in bigCategories)
                Console.WriteLine($"{g.Key}: {g.Count()} products");

            // =====

            var customersByCountry = from c in Source.CustomerList
                                     group c by c.Country into g
                                     select new
                                     {
                                         Country = g.Key,
                                         Count = g.Count(),
                                         TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                                     };
            foreach (var item in customersByCountry)
                Console.WriteLine(item);

            // =====

            int totalUnits = Source.ProductList.Sum(p => p.UnitsInStock);
            Console.WriteLine(totalUnits);

            // =====

            decimal cheapest = Source.ProductList.Min(p => p.UnitPrice);
            decimal mostExpensive = Source.ProductList.Max(p => p.UnitPrice);
            Console.WriteLine($"Cheapest: {cheapest:F2}");
            Console.WriteLine($"Most Expensive: {mostExpensive:F2}");

            // =====

            var categories = Source.ProductList.Select(p => p.Category).Distinct();
            foreach (var category in categories)
                Console.WriteLine(category);

            // =====

            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };
            var onlyInA = setA.Except(setB);
            foreach (var id in onlyInA)
                Console.WriteLine(id);

            // =====

            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };
            var onlyInList1 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);
            foreach (var country in onlyInList1)
                Console.WriteLine(country);

            // =====

            Dictionary<int, Product> productsById = Source.ProductList.ToDictionary(p => p.ProductID);
            Product product18 = productsById[18];
            Console.WriteLine($"{product18.ProductID} - {product18.ProductName} - {product18.UnitPrice:F2}");

            // =====

            var firstOver50 = Source.ProductList.First(p => p.UnitPrice > 50);
            Console.WriteLine($"{firstOver50.ProductName} - {firstOver50.UnitPrice:F2}");

            // =====

            var firstOver500 = Source.ProductList.FirstOrDefault(p => p.UnitPrice > 500);
            Console.WriteLine(firstOver500?.ProductName ?? "null");

            // =====

            var tableOf7 = Enumerable.Range(1, 10).Select(i => $"7 x {i} = {7 * i}");
            foreach (var row in tableOf7)
                Console.WriteLine(row);

            // =====

            var evenNumbers = Enumerable.Range(1, 30).Where(n => n % 2 == 0);
            foreach (var n in evenNumbers)
                Console.WriteLine(n);

            // =====

            var combinedNames = Source.ProductList.Take(3).Select(p => p.ProductName)
                .Concat(Source.CustomerList.Take(3).Select(c => c.CustomerName));
            foreach (var name in combinedNames)
                Console.WriteLine(name);

            // =====

            var sales = Source.ProductList.Zip(Source.CustomerList, (p, c) => $"{p.ProductName} sold to {c.CustomerName}");
            foreach (var line in sales)
                Console.WriteLine(line);
        }
    }
}
