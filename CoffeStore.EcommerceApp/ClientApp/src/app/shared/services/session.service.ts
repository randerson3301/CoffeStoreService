import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class SessionService {

  setSessionKeys(name: string, token: string) {
    sessionStorage.setItem("name", name);
    sessionStorage.setItem("token", token);
  }

  getUserId(): string | null {
    const token: string = sessionStorage.getItem("token") || "";
    const decode = jwtDecode<any>(token);
    return decode.UserId;
  }

  getName(): string | null {
    return sessionStorage.getItem("name");
  }

  clearSession() {
    sessionStorage.clear();
  }
}
