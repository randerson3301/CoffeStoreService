import { Component } from '@angular/core';
import { faUserCircle, faShoppingCart } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  faUserCircle = faUserCircle;
  faShoppingCart = faShoppingCart;
  logged = true;
  
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
