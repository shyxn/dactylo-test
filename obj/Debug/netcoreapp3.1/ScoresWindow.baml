   M S B A M L   `   `   `  ����F  BDactyloTest, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DactyloTestDactyloTest  5   	   )clr-namespace:DactyloTestDactyloTest  5   	       DactyloTest.ScoresWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DactyloTest     5   	   -controlclr-namespace:DactyloTest     5   	   -   5          � Title$  Scores - DactyloTest��5	   	   $	��641��6&   $
��1284��63    � WindowStartupLocation$ CenterScreen=�6@   ���#212F3D � 
ResizeMode$ CanMinimize=�6�   .��5
      � .+�5   
   �� $
��Auto=�6   .��5      �� $��15q�6   .+�5      �� -   5         ��Name$ Titles��5      $	��138��6)   .+�5      �� -   5         ��Name$ ChangeNickname��5       �� Left$	 281��65   
 �� Top$ 84��6G   $|�False��6W   ��5      �� 5      .R�5   "   �� $ ��/icons8-utilisateur-48.png��6(   $��24��6L   $��28��6X   $ 7��6c   $ 9��6s   5      5      �� 5      -   5         ��Name$ nickname��5      $	 309��6.   $ 84��6@   $	��172��6P   $��24��6\   $��Poppins)�6h   
�������$��11(�6�   $��0,4,0,0q�6�   $�� ��6�   �� 5      -   5         ��Name$ Titre��5       $��Poppins)�6/   $��40(�6D   
�������$��15,0,0,0q�6e   $
��Bold"�6w   Tableau des scores6�   �� 5      -   5        $ OnlyMyScores��5      $	 131��6K   $ 79��6]   $	��128��6m   $��35��6y      RoundBasicBtn#��[  6�   .��6�   
Mes scores6�   �� 5      -   5        $ 	AllScores��5      $��92��6H   $
��Tous��6S   $ 79��6b   $ 15��6r   $��35��6�     SelectedRoundBtn#��[ 6�   �� 5      -   5        $ 
QuitScores��5      $	��150��6J   $��Revenir au jeu��6V   $ 12��6o   $
 1057��6   $��36��6�   #��[  6�   �� 5      -	   5        $ ShowGraphOrTable��5      $	��131��6O   $��	GRAPHIQUE��6[   $
��Bold"�6o   $ 12��6�   $	 530��6�   $��36��6�   #��[  6�   5      � 5      -
   5        	 �Name$	 
scoreTable��5      5         DactyloTest.GeneralGraph  5      -   5        
  Name$
 GeneralGraph��5   '   $y�Hidden=�6=   $
��Left=�6Q   !   DactyloTest.IndividualGraph  5       -   5          Name$ IndividualGraph��5    *   $y�Hidden=�6C   $
��Left=�6W   5!      5"      5#      5$      