   M S B A M L   `   `   `  ���� F  BDactyloTest, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DactyloTestDactyloTest      DactyloTest.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:DactyloTest     -     � Title$  DactyloTest vx.x.x��$	��453��$	��800�����#212F3D � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=�.���� .+��� -    ��Name$ 
gameCanvas��$y�Visible=�.+��� -    ��Name$ nickname�� �� Left$ 42��
 �� Top$ 9��$	��172��$��24��   Poppins#��[  
�������$��11(�$��0,4,0,0q�$�� ���� -    ��Name$ 	leftChars��#��[  
�������$��22(�$��30��$��81��$	 655��$ 57���� -    ��Name$ InputTextBox��$��23(�	 �� TextAlignment$	 Center=�$��1��$	 796��$	 236���� ���#212F3D$��40��$��40��$	 757��$	 232���� $��
0, 0, 0, 2q�
�������
������ $	 169��$ 56��$	��461��._��� 
 �� Orientation$
 
Horizontal=�$��Center=�.+�� $��Center=�.+��� -   $ leftSideText��  Source Code Pro#��[ $	��220��$��Center=� �� TextAlignment$ Right=�$��Center=�$��CharacterEllipsis=�$��LeftToRight=�$��22(�$��33��$��DemiBold"�
��������� -   $ midSideText��#��[ $��Center=�$��22(�$��DemiBold"�$ Center=�$��33��$��Normal&�$��18�����	#FF45607C
��������� -   $ rightSideText��#��[ $	��220��$��Center=�$��CharacterEllipsis=�$��22(�$��33��$��DemiBold"�
��������� $��
1, 0, 0, 1q�
�������
������ $	��276��$ 82��$	 146��._��� $��10,0,0,0q�.+��� -	   $ 	totalTime��  
StatTxtBox#��[ �� -
   $ totalStrokes��#��[ �� -   $ totalCorrectStrokes��#��[ �� -   $ totalIncorrectStrokes��#��[ �� �� -   $ currentWPMTitle��$��SemiBold"�#��[ $y�Hidden=�Mots/minute : �� -   $ 
currentWPM��$��SemiBold"����#FA8334#��[ �� �� -   $ currentAccuracyTitle��$��SemiBold"�#��[ $y�Hidden=�Précision : �� -   $ currentAccuracy��$��SemiBold"����#FA8334#��[ �� -    ��Name$ quitBtn��$��86��$ 28��$	 350��  RoundBasicBtn#��[ $��44��.��	Quitter�� -   $ 
restartBtn��$	��149��$	 483��$	 209��#��[ .��Recommencer�� -   $ changeModeBtn��$	��222��$	 483��$	 259��#��[ .��Changer le mode de jeu�� -   $ scoreBtn��$	��114��$��28��$��13(�$ 14��$ 46��#��[ .��Voir les scores�� -   $ anotherTextBtn��#��[ $	��207��$	 483��$	 159��.��Générer un autre texte�� -   $ ChangeNickname��$ 14��$ 9��$|�False������ .R��� $ ��/icons8-utilisateur-48.png��$��24��$��28��$ 7��$ 9���� -    ��Name$ nicknameInputCanvas��$y�	Collapsed=�$��5q�$
}�True�� �� CornerRadius$ 5��$	 263��$	 135�����	#FFBBCAD8._��� $	��158��$	��264�����	#FFBBCAD8.+��� -   $ inputNickname��$	��168��$ 48��$ 61��$��30��#��[  $	 Center=�$��13(�$��0,5,0,0q�$��Center=�$��Center=�$��0,0,0,0q�.�� � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 14��.���� -   $ confirmNicknameBtn��$ 88��$	 113��  ValidateNicknameBtn#��[ .��	Valider�� $ 26��$ 12��#��[  $��18(�$
��Bold"�$��36��$	��212��$��Center=�$��Center=�.��Entrez un pseudo :�� -   $ selectModeCanvas��$y�	Collapsed=�$��5q�$
}�True��$ 5��$	 166��$ 75�����	#FFBBCAD8$	��288��$	��468��._��� ���	#FFBBCAD8.+��� -   $ confirmModeBtn��$	 185��$	 232��  ValidateModeBtn#��[ ${�False��.��	Valider�� $	 112��$ 10��#��[  $��18(�$
��Bold"����	#FF212F3D$��36��$	��234��$��Center=�.��Choisir un mode de jeu�� -   $ timeModePanel��$��2q�$
}�True��$ 5��$ 22��$ 60�����	#FF212F3D$
��Auto��$	��186��._��� .+��� #��[  $��12(�$
��Bold"����	#FF212F3D$��Center=�$��5q�$��
Mode temps���� #��[  $��11(�$��54��$��Center=�$��10q�$��WrapWithOverflow=�$ Center=��� $7�1La durée du test n'excédera pas le temps choisi����  � Language$ fr-ch�$�.���� -   $ 
timeCanvas��$��42��$y�	Collapsed=�.+��� -   $ UpMinBtn��$��24��$��18��$��10(�$	��▲��$ 48��$	 -28����Z� ��Z� �� -   $ UpSecBtn��$��24��$	 102��$	 -28��$��18��$��10(�$	��▲����Z� ��Z� �� -    $ 
DownMinBtn��$��24��$��18��$��10(�$	��▼��$ 48��$ 10����Z� ��Z� �� -!   $ 
DownSecBtn��$��24��$��18��$��10(�$	��▼��$	 102��$ 10����Z� ��Z� �� -"   $ 
MinutesLbl��$ 47��$	 -13��$��23��#��[  .��01�� $ 69��$	 -13��$��23��$
��Bold"�#��[  .��min�� -#   $ 
SecondsLbl��$	 101��$	 -13��$��23��#��[  .��00�� $	 123��$	 -13��$��23��$
��Bold"�#��[  .��s�� -$   $ textModePanel��$��2q�$
}�True��$ 5��$	 246��$ 60�����	#FF212F3D$	��116��$	��186��._��� .+��� #��[  $��12(�$
��Bold"����	#FF212F3D$��Center=�$��5q�$��
Mode texte���� #��[  $��11(�$��54��$��Center=�$��10q�$��WrapWithOverflow=�$ Center=��� $L�FLe texte devra être écrit dans son intégralité pour finir le test.���� -%   $ title��#��[  
�������$��11(�$��30��$	��496��$	 268��$	 382��$ Right=�