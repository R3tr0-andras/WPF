   M S B A M L   `   `   `  ����	  Prism     Prism.Controls.MainContent   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .��5      �� ���#606060$��0q�5      ._�5
   
   �   � RowDefinitions  5      �� 5      $��* �6    $	��200��6+   �� 5      $
��Auto �6    $��35��6.   .+�5      �� $��0��6   ���#606060$��0q�5      ._�5      �� $
��Auto=�6   $
��Auto=�5      $��20q�5      .��5      � .+�5      (�  (� Stretch$ Uniform=�6"   ��5   "   �� *ActualWidth+ �� RelativeSource ��  �� AncestorType ScrollViewer��5   "   �� *ActualHeight+ ��  ScrollViewer.��5      �� $��Center=�6)   $��Center=�5   )   .+�5   "   �� $��Zone de contenu principal��6,   
�������$��24(�5   ,   $��Light"�5    ,   $��Center=�5!   ,   $��0,0,0,10q�5"   ,   �� 5#   "   $/��)Cette zone s'adapte à toutes les tailles��6,   ���	#CCFFFFFF$��14(�5%   ,   $��Center=�5&   ,   5'      5(      5)      5*      5+      �� 5.      $��1��6   ���#D32F2F$��40��50      ._�51      � $
��15,0q�6    � ColumnDefinitions 52      �� 53      $��* �6+   �� 54      $
��Auto �6+   .+�57      �� $��0��6!    �� Orientation$ 
Horizontal=�58   !   $��Center=�59   !   .+�5:      �� $��
↵ Ouvrir��6$   
�������$��12(�5<   $   $��0,0,15,0q�5=   $   �� 5>      $��
Esc Fermer��6$   
�������$��12(�5@   $   5A      �� 5C      $��1��6    ���	#CCFFFFFF$��10(�5F       $��Center=�5G        J� Opacity$	 0.7Q�5H       ��5D       �� *ActualWidth+ �� 
 Window �� StringFormat$ 	L: {0:F0}��5H       5I      5J      5K      5L      5M      