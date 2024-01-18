# receipt-parser

Since this is a simple Console Application project, there is no Code Spectrum here.<br>
But you can read my thinking process down below.

## Thinking Process

> Request: Requested output data (correct output)
> 
> Response: receipt-parser output data (solution output)

Request
```
1 TEŞEKKÜRLER
2 XXXX TEKS. GIDA INS SAN. LTD.STI.
...
```
Response, 
```
1 TEŞEKKÜRLER
2 XXXX GIDA
3 TEKS. XAX XBX Limited
...
```
* If we take a look at the data, we see that either this data has been manipulated or it is sender's responsibility to make them in order.
* Coordinates are the top left corner of the related text box (first vertice values in the BoundingPoly object) they even should not be intercept, otherwise both text will be overriden.
```
(36, 88) 	 TEŞEKKÜRLER   -> Possible overriden text.
(36, 114) 	 XXXX        -> Possible overriden text.
(242, 116) 	 GIDA
(165, 120) 	 TEKS. -> X value should be in between (242 < x < 307)
(307, 122) 	 XAX
(349, 123) 	 XBX
(408, 123) 	 Limited
```
* Same here
```
Request
16 0,333 KGx 4,99
17 XXXX YYYY ZZZZ 08 *1,77
18 4

Response
16 0,333 KGx 4,99
17 YYYY
18 XXXX ZZZZ 08 *1,77
19 4

(40, 524) 	 0,333
(116, 525) 	 KGx
(166, 526) 	 4,99
(116, 550) 	 YYYY   -> X value should be lower than 166 ( x < 166 ) - And yes it is.
(40, 551) 	 XXXX   -> X value should be in between (116 < x < 190)
(190, 551) 	 ZZZZ
(336, 554) 	 08
(496, 555) 	 *1,77
(53, 576) 	 4
```
* Same here
```
Request
25 08 *0,11 *55,55
26 CASHIER : DUMMYNAME

Response
25 08 *0,11 *55,55
26 :
27 CASHIER DUMMYNAME

(64, 816) 	 08
(224, 816) 	 *0,11
(406, 818) 	 *55,55
(164, 862) 	 :  -> 
(64, 863) 	 CASHIER -> X values should be swapped.
(186, 863) 	 DUMMYNAME
```
And finally here
```
Request
30 2 NO:0000 EKÜ NO:0000

Response
30 2 EKÜ
31 NO:0000 NO:0000

(73, 1032) 	 2
(416, 1033) 	 EKÜ   ->
(86, 1034) 	 NO:0000 -> X values should be swapped.
(477, 1038) 	 NO:0000

```
Actually if you take a closer look, all problematic parts are just ```x value swap```

```
If you swap those data's x values with its neighbour, the algorithm works exactly as expected.

-> (242, 116) 	 GIDA
-> (165, 120) 	 TEKS. 

-> (116, 550) 	 YYYY  
-> (40, 551) 	 XXXX  

-> (164, 862) 	 :   
-> (64, 863) 	 CASHIER 

-> (416, 1033) 	 EKÜ   
-> (86, 1034) 	 NO:0000 
```

* Alternatively, we could have sorted the data based on the centroid of the rectangle obtained using all four points. 
* However, due to not knowing whether the data has been manipulated (if it has been manipulated and is in the way I described above, there is no need for further development) and because it is only an averaging the numbers, it was not added to the project due to time constraints.
* If the data has not been manipulated, all four points output more precise solution.
* If the data has not been manipulated and after the all four points calculations if it still behave similar, the responsibility lies with the sender of the data.
* The data does not fit our algorithm, does not seem important.
