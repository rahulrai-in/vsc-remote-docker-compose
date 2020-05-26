package main

import (
	"fmt"
	"math/rand"
	"net/http"
	"os"
	"time"
)

func main() {

	portNumber := os.Getenv("PORT")
	http.HandleFunc("/pong", func(w http.ResponseWriter, r *http.Request) {
		time.Sleep(time.Duration(rand.Intn(10 - 1) + 1) * time.Second)
		fmt.Fprintf(w, "Pong!")
	})

	fmt.Println("Server listening on port ", portNumber)
	err := http.ListenAndServe(":"+portNumber, nil)
	if err != nil {
		fmt.Printf("Failure %s", err.Error())
	}
}
