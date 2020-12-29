import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  baseUrl: String = 'http://localhost:5000/admin/location/'
  
  constructor(private http: HttpClient) { }

  getAll() {
    let headers = this.generateAuthHeaders()
    return this.http.get(this.baseUrl + 'all', { headers: headers})
        .pipe((response: any) => {
            return response;
        });
  }

  delete(id) {
    let headers = this.generateAuthHeaders()
    return this.http.delete(this.baseUrl + `${id}`, { headers: headers })
        .pipe((response: any) => {
          return response;
        })
  }

  addNew(model: any) {
    let headers = this.generateAuthHeaders();

    return this.http.post(this.baseUrl + 'new', model, { headers: headers})
      .pipe((response : any) => {
        return response;
      })
  }
  
  private generateAuthHeaders() {
    let userItem = localStorage.getItem('user');
    let token = null;
    if (userItem) {
      token = JSON.parse(userItem).token;
    }
   
    var headers = new HttpHeaders(
      { 'Authorization': `Bearer ${token}` }
    )

    return headers;
  }
}
