import { Component, OnInit } from '@angular/core';
import { Shop } from './model/shop.model';

@Component({
  selector: 'app-visit-us',
  templateUrl: './visit-us.component.html',
  styleUrls: ['./visit-us.component.css'],
})
export class VisitUsComponent implements OnInit {
  public shops: Shop[] = [];

  constructor() {}

  ngOnInit(): void {
    this.shops.push({
      name: 'Loja São Roque',
      fullAddress: 'Rua Mathias Leme de Barros, 123, Centro, São Roque, SP',
    });

    // this.shops.push({
    //   name: 'Loja Parque Viana',
    //   fullAddress: 'Avenida Anibal Correia, 629, Votupoca, Barueri, SP',
    // });

    // this.shops.push({
    //   name: 'Loja Primavera',
    //   fullAddress: 'Rua das Flores, 123, Jardim Primavera, São Paulo, SP',
    // });
  }
}
