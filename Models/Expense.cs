using System;

// This class represents ONE expense in our application
// Think:
//
// Rent = one expense
// Car payment = one expense
// Groceries = one expense

public class Expense
{
    // Unique ID number for each expense
    // Example:
    //
    // Expense 1
    // Expense 2
    // Expense 3
    //
    // Databases use this as the primary key

    public int Id { get; set; }


    // Stores expense title
    //
    // Examples:
    // "Rent"
    // "Groceries"
    // "Phone Bill"

    public string Title { get; set; }


    // Stores dollar amount
    //
    // Example:
    // 2300.50

    public decimal Amount { get; set; }


    // Stores category

    public string Category { get; set; }

    // Stores date expense occurred
    //
    // Example:
    // 5/17/2026

    public DateTime Date { get; set; }


    // Optional notes

    public string Notes { get; set; }
}
