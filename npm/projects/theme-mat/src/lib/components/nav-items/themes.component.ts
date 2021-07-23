import { Component, OnInit } from '@angular/core';
import { DigniteThemeService } from '../../services/theme.service';
import { ThemeMatTheme } from '../../tokens';

@Component({
    selector: 'dignite-themes',
    templateUrl: './themes.component.html',
})
export class ThemesComponent implements OnInit {

    themes: ThemeMatTheme[];
    constructor(
        private themeService: DigniteThemeService,
    ) {
    }

    ngOnInit() {
        this.themes = this.themeService.getThemes();
    }

    setTheme(theme: ThemeMatTheme) {
        this.themeService.setTheme(theme.name);
    }
}
