import { ConfigStateService, LanguageInfo, SessionStateService } from '@abp/ng.core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import snq from 'snq';

@Component({
  selector: 'dignite-languages',
  templateUrl: './languages.component.html'
})
export class LanguagesComponent {

  languages$: Observable<LanguageInfo[]> = this.configState.getDeep$('localization.languages');

  get defaultLanguage$(): Observable<string> {
    return this.languages$.pipe(
      map(
        languages =>
          snq(
            () => languages.find(lang => lang.cultureName === this.selectedLangCulture).displayName,
          ),
        '',
      ),
    );
  }

  get dropdownLanguages$(): Observable<LanguageInfo[]> {
    return this.languages$.pipe(
      map(
        languages =>
          snq(() => languages.filter(lang => lang.cultureName !== this.selectedLangCulture)),
        [],
      ),
    );
  }

  get selectedLangCulture(): string {
    return this.sessionState.getLanguage();
  }

  constructor(private sessionState: SessionStateService, private configState: ConfigStateService) { }

  onChangeLang(cultureName: string) {
    this.sessionState.setLanguage(cultureName);
  }
}
