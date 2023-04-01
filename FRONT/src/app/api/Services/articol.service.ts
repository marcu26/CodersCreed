import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class ArticolService {
  constructor(private http: HttpClient) { }

  getLibraryArticole(): Observable<any[]> {
    return this.http.get<any[]>(environment.URL + "/Articol/GetLibrary");
  }

  getAutori(): Observable<any[]> {
    return this.http.get<any[]>(environment.URL + "/Articol/GetAuthors");
  }

  getUserBookshelf(id: number): Observable<any[]> {
    return this.http.get<any[]>(environment.URL + "/Articol/GetBookshelf?ID=" + id);
  }

  saveNewBook(book: any) {
    book.id = 0;
    book.idPublicatie = 0;
    return this.http.post(environment.URL + "/Articol/AddNewBook", book)
  }

  updateExisitingBook(book: any) {
    return this.http.put(environment.URL + "/Articol/UpdateExistingBook", book)
  }

  removeBook(bookID: any) {
    return this.http.delete(environment.URL + "/Articol/RemoveBook?ID=" + bookID)
  }
}
