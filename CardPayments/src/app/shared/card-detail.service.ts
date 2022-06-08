import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CardDetail } from './card-detail.module';

@Injectable({
  providedIn: 'root'
})
export class CardDetailService {

  constructor(private http: HttpClient) { }

  readonly baseUrl = 'http://localhost:5000/api/CardDetail';
  formData: CardDetail = new CardDetail();
  list: CardDetail[];

  postCardDetail() {
    return this.http.post(this.baseUrl, this.formData);
  }

  putCardDetail() {
    return this.http.put(`${this.baseUrl}/${this.formData.cardDetailId}`, this.formData);
  }

  deleteCardDetail(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  refreshData() {
    this.http.get(this.baseUrl)
      .toPromise()
      .then(res => this.list = res as CardDetail[]);
  }
}
