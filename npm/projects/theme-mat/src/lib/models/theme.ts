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
     * 为了防止主题冲突,addNameToBody和styleHref只能设置一个
     * 是否将name添加到body用于主题切换
     */
    addNameToBody?: boolean;
    /**
     * 为了防止主题冲突,addNameToBody和styleHref只能设置一个
     * 用于懒加载的样式表,样式表地址
     */
    styleHref?: string;
    /**
     * 是否是默认主题
     */
    isDefault?: boolean;
    /**
     * 主要色调
     */
    primaryColor: string;
}
/**
 * 白色模式 黑夜模式
 */
export type ThemeMatThemeType = 'light' | 'dark';
