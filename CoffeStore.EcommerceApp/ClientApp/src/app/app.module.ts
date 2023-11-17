import { BrowserModule } from '@angular/platform-browser';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { NewAddressComponent } from './new-address/new-address.component';
import { EditAccountComponent } from './edit-account/edit-account.component';
import { EditPasswordComponent } from './edit-password/edit-password.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { InputErrorsComponent } from './shared/input-errors/input-errors.component';
import { SuccessAlertComponent } from './shared/success-alert/success-alert.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = {};


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
    NewAddressComponent,
    EditAccountComponent,
    EditPasswordComponent,
    MyOrdersComponent,
    InputErrorsComponent,
    SuccessAlertComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskModule.forRoot(options),
    SweetAlert2Module.forRoot(),
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
      { path: 'new-address', component: NewAddressComponent },
      { path: 'edit-account', component: EditAccountComponent },
      { path: 'edit-password', component: EditPasswordComponent },
      { path: 'my-orders', component: MyOrdersComponent },
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
