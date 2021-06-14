export interface BookForInventory {
    id: string,
    title: string,
    isbn: string,
    author: string,
    year: number,
    price: number,
    numberOfPages: number,
    quantity: number,
    numberOfPurchases: number
    quantityLimit: number
}