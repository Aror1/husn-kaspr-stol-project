export interface Product {
  id: number;
  name: string;
  price: number;
  image?: string;
}

export interface CartItem {
  id: number;
  name: string;
  price: number;
  quantity: number;
}

export interface Category {
  name: string;
  icon: string;
  label: string;
}
