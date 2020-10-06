package br.ufscar.sead.loa.scalear.remar

class Model3D {

    String name
    String source
    String author

    long ownerId
    String taskId

    static constraints = {
        name blank: false, maxSize: 150
        source blank: false, maxSize: 500
        author blank: false
        ownerId blank: false, nullable: false
        taskId nullable: true
    }
}
