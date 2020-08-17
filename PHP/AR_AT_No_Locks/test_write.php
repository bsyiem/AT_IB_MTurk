<?php
echo `whoami`;

if(($file = fopen("test.csv", "a")) !==FALSE){
	fputcsv($file,array("1","test"));
	fclose($file);
}else{
	echo "error";
}
?>