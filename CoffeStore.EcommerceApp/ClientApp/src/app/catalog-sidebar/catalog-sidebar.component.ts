import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-catalog-sidebar',
  templateUrl: './catalog-sidebar.component.html',
  styleUrls: ['./catalog-sidebar.component.css']
})
export class CatalogSidebarComponent implements OnInit {

  constructor(private route: Router) { }

  ngOnInit(): void {
  }  

  public isCatalogRoute(): boolean {
    return this.route.url === '/catalog';
  }
}
