<?php

	header("Access-Control-Allow-Origin: *");
	/*
	if(($file = fopen("../files/last_pid.csv","r")) !== FALSE)
	{
		$data = fgetcsv($file,10,",");
		if($data !== FALSE)
		{
			echo($data[0]);
		}
	}	
	fclose($file);
	*/
	
	if(($file = fopen("../../files/Pilot/last_pid.csv","r")) !== FALSE)
	{
		$data = fgetcsv($file,10,",");
		if($data !== FALSE)
		{
			echo($data[0]);
		}	
		fclose($file);
	}else{
		echo "Error: Please Reload Page";
	}	
?>
