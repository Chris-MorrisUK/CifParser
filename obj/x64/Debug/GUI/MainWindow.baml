   M S B A M L   `   `   `  ����F  BScheduleVis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ScheduleVisScheduleVis  5   	       ScheduleVis.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1ScheduleVisclr-namespace:ScheduleVis     5   	     � Title$  
MainWindow��5   	   $	��350��6   $
��1024��6)    � WindowState$ 	Maximized=�66    � 	Resources 5         	BoolToVis��%   &	        �� 5   
   6'   5       � DataContext 5	      %   ScheduleVis.MainWindowViewModel  5
   
   .��5      � .+�5   
   �� $��Visible=�6   $��0,40,0,0q�6>   .��5      �� -   5         ��Name$ 	txtOutput��5      ��6+   �� *
OutputText+ �� Mode$ OneWay=�5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��40��6/   $	��Top=�6;   .+�5      �� -   5        $��btnImportStations��5   [   $��Import Station Names��6   $��30��64   $
��Left=�6@   $	��Top=�6t   $	��140��6�   $��5q�6�   �� 5      -   5        $��btnImportSchedules��5   W   $��Import Schedules��6   $��30��60   $
��Left=�6<   $	��Top=�6q   $	��140��6�   $��5q�6�   �� 5      -   5        $��btnCombinedImport��5   V   $��Combined Import��6   $��30��6/   $
��Left=�6;   $	��Top=�6o   $	��140��6�   $��5q�6�   �� 5      -   5        $��tbNameLabel��5   ?   $��23��6   $
��Left=�6$   $��Center=�6R   $	��172��6m   $��5q�6y   Person responsible for update6�   �� 5      -   5        $��txtName��5   =   $��23��6   $
��Left=�6"   $��Center=�6L   $	��172��6g   $��5q�6s   	� 5      -   5        $��rbUri��5   ]   $	��Uri��6   $��16��6(   $
��Left=�64   $	��5,7q�6P   $��Center=�6j    	� 	GroupName$ NameType��6�   	� 5      -   5        $��rbLit��5   `   $��Literal��6   $��16��6,   $
��Left=�68   $	��5,7q�6S   $��Center=�6m    d� 	IsChecked$
 TrueY�6�   $ NameType��6�   �� 5      -	   5        	 ��Name$	 chkIncludeProv��5      $��Include Prov��6/   $	��5,7q�6G   $��Center=�6T   $
��Left=�6o   �� 5      -
   5        
 ��Name$
 btnSaveOutput��5      $��Save Messages��6,   $	��Top=�6D   $	��140��6\   $��5q�6�   5      �� 5   
   -   5         ��Name$ txtStatusMessages��5      $��Center=�6-   $��Center=�6J    �� TextAlignment$ Center=�6e   $	��400��6|   $��30��6�   $��	0,-50,0,0q�6�   
������ y�6�   �� *ProgressVisible+ �� 	Converter# [  $ OneWay=�� 5   
   -   5         �Name$ progress��5   h   $��Center=�6   $��30��63   $��Center=�6@   $	��400��6[    � Maximum$	 255Q�6z   y�6�   �� *ProgressVisible+# [  $ OneWay=� � Value 6�   �� *ProgressVisible+$ OneWay=�5      5      