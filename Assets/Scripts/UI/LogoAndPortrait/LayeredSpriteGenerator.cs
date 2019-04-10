using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LayeredSpriteGenerator {
  
    private static List<Color> layerColors = new List<Color>() {
        Color.red,
        Color.magenta,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.white,
        Color.black
    };
    
    public static LayeredColoredSprite[] GenerateLayeredSprites(List<string> layerNames, List<Color> layerColors, int number=1) {
        List<List<Sprite>> spriteLayers = new List<List<Sprite>>();
        foreach (string s in layerNames) {
            List<Sprite> sprites = SpriteLoader.GetAllSymbolParts(s);
            if(sprites != null && sprites.Count > 0) {
                spriteLayers.Add(sprites);
            }
        }
        LayeredColoredSprite[] toReturn = new LayeredColoredSprite[number];
        for(int i = 0; i < number; i++) {
            toReturn[i] = new LayeredColoredSprite(spriteLayers.Count);
            for(int j = 0; j < spriteLayers.Count; j++) {
                toReturn[i].SetSprite(j, spriteLayers[j][Random.Range(0, spriteLayers[j].Count)]);
                toReturn[i].SetColor(j, layerColors[Random.Range(0, layerColors.Count)]);
            }
        }
        return toReturn;
    }
}
