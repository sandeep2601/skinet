import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ShopService } from '../shop/shop.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  productForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
    description: new FormControl('', [Validators.required, Validators.maxLength(250)]),
    price: new FormControl('', [Validators.required, Validators.max(9999999999)]),
    pictureUrl: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    productTypeId: new FormControl('', [Validators.required, Validators.max(20)]),
    productBrandId: new FormControl('', [Validators.required, Validators.max(40)]),
  });

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.shopService.saveProduct(this.productForm.value).subscribe(response => {
      this.productForm.reset({});
    });
    console.warn(this.productForm.value);
  }
}
