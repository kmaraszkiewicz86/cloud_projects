import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Observable, throwError } from 'rxjs'
import { tap, catchError } from 'rxjs/operators'

import { AwsPhotoGalery } from "./aws-photo-galery";

@Injectable({
  providedIn: 'root'
})
export class DynamoDbServiceService {

  private baseUrl = "http://192.168.0.23:5000/"

  constructor(private http : HttpClient) { }

  getAll(): Observable<AwsPhotoGalery[]> {
    return this.http.get<AwsPhotoGalery[]>(this.baseUrl).pipe(
      tap(data => console.log(data)),
      catchError(catchError)
    )
  }

  private catchError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }

    console.log(errorMessage)
    return throwError(errorMessage);
  }
}
