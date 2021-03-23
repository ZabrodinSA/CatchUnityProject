const express = require('express')
const app = express()
app.use(express.json())

const PORT = process.env.PORT ?? 8080

let player = []

app.get('/player', (req, res) => {
   res.send(player)
})

app.post('/player/create', (req, res)=>{
   let newPlayer = {
      "id": req.body.id,
      "name": req.body.name,
      "score": req.body.score
   }
   player.push(newPlayer)
   player.sort((a,b)=>{
      if (a.score<b.score){
         return 1
      }
      if (a.score>b.score){
         return -1
      }
      return 0
   })
   player.forEach ((item_one, index_one)=>{
      player.forEach((item, index_two)=>{
              if (item_one.name == item.name ){
                  if (index_one!=index_two){
                      if (item_one.score<item.score){
                        player.splice(index_one, 1)
                      }
                      else {
                        player.splice(index_two, 1)
                      }
                  }
              }
      })
  })
   console.log(player)
   res.send(player)
})

app.delete('/player/delete', (req, res)=>{
   let deletePlayer = req.body
})

app.listen(PORT, ()=>{
   console.log(`listening on ${PORT}...`)
})

                                            

