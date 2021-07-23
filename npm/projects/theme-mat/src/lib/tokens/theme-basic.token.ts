import { InjectionToken } from '@angular/core';

export const THEME_MAT_OPTIONS = new InjectionToken('THEME_MAT_OPTIONS');

export interface ThemeMatOptions {
    themes?: ThemeMatTheme[];
}

export interface ThemeMatTheme {
    /**
     * 中文名字
     */
    displayName: string;
    /**
     * 名称
     */
    name: string;
    /**
     * 主题类型
     */
    type: ThemeMatThemeType;
    /**
     * body上的主题名字
     */
    addNameToBody?: boolean;
    /**
     * 样式表地址，仅限未有授权约束地址
     */
    styleHref?: string;
    /**
     * 主要色调
     */
    primaryColor: string;
    /**
     * 是否是默认主题
     */
    isDefault?: boolean;
}
/**
 * 白色模式 黑夜模式
 */
export type ThemeMatThemeType = 'light' | 'dark';
