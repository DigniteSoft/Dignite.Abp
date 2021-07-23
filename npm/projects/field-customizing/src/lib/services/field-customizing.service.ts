import { InternalStore, reloadRoute } from '@abp/ng.core';
import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FieldCustomizing } from '../models/field-customizing';

@Injectable({
  providedIn: 'root'
})
export class FieldCustomizingService {
  private readonly store: InternalStore<FieldCustomizing.FormProvider[]>;

  get formProviders$(): Observable<FieldCustomizing.FormProvider[]> {
    return this.store.sliceState(state => state);
  }

  get formProviders(): FieldCustomizing.FormProvider[] {
    return this.store.state;
  }

  get onUpdate$(): Observable<FieldCustomizing.FormProvider[]> {
    return this.store.sliceUpdate(state => state);
  }

  constructor(private ngZone: NgZone, private router: Router) {
    this.store = new InternalStore([]);
  }

  addes(formProviders: FieldCustomizing.FormProvider[], reload?: boolean): void {
    for (const formProvider of formProviders) {
      this.add(formProvider);
    }
  }

  add(formProvider: FieldCustomizing.FormProvider, reload?: boolean) {
    const formProviders = [...this.store.state];

    const index = formProviders.findIndex(
      component => component.name === formProvider.name,
    );

    if (index > -1) {
      formProviders[index] = formProvider;
    } else {
      formProviders.push(formProvider);
    }

    this.store.set(formProviders);

    if (reload) {
      reloadRoute(this.router, this.ngZone);
    }
  }

  get(formProviderName: string, useType: FieldCustomizing.FormProviderUseType): FieldCustomizing.FormProvider {
    return this.formProviders.find(provider => provider.name === formProviderName && provider.useType === useType);
  }

  get$(formProviderName: string, useType: FieldCustomizing.FormProviderUseType): Observable<FieldCustomizing.FormProvider> {
    return this.formProviders$.pipe(
      map(components => components.find(provider => provider.name === formProviderName && provider.useType === useType)),
    );
  }
}
