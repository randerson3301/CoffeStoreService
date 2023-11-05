import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { FooterComponent } from './footer/footer.component';
import { CatalogComponent } from './catalog/catalog.component';
import { ProductComponent } from './product/product.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { VisitUsComponent } from './visit-us/visit-us.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CatalogSidebarComponent } from './catalog-sidebar/catalog-sidebar.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';
import { FormUserDataComponent } from './shared/form-user-data/form-user-data.component';
import { FormUserPasswordComponent } from './shared/form-user-password/form-user-password.component';
import { FormUserAddressComponent } from './shared/form-user-address/form-user-address.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    FooterComponent,
    CatalogComponent,
    ProductComponent,
    VisitUsComponent,
    AboutUsComponent,
    LoginComponent,
    RegisterComponent,
    CatalogSidebarComponent,
    MyAccountComponent,
    ShoppingCartComponent,
    FormUserDataComponent,
    FormUserPasswordComponent,
    FormUserAddressComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'catalog', component: CatalogComponent },
      { path: 'product/:id', component: ProductComponent },
      { path: 'visit-us', component: VisitUsComponent },
      { path: 'about-us', component: AboutUsComponent },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'my-account', component: MyAccountComponent },
      { path: 'shopping-cart', component: ShoppingCartComponent },
    ]),
    NgbModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  constructor() {
  }
}